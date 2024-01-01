// service-worker.js

// Cache name
const CACHE_NAME = 'altfuture-cache-v1';
const MAX_CACHE_AGE = 60 * 1000; 
const urlsToCache = [
    '/',
    '/css/site.css',
    '/js/site.js',
    '/Home/About',
    '/Portfolios/Dashboard'
];

// Install a service worker
self.addEventListener('install', event => {
    // Perform install steps
    event.waitUntil(
        caches.open(CACHE_NAME)
            .then(cache => {
                console.log('Opened cache');
                return cache.addAll(urlsToCache);
            })
    );
});

// Fetch Cache and/or Network response
self.addEventListener('fetch', event => {
    event.respondWith(
        caches.open(CACHE_NAME).then(cache => {
            return cache.match(event.request).then(cacheResponse => {
                // Check if the cache is available and not older than 1 minute
                if (cacheResponse) {
                    const fetchTime = cacheResponse.headers.get('sw-fetch-time');
                    if (fetchTime && (Date.now() - fetchTime) <= MAX_CACHE_AGE) {
                        console.log('Cache loaded.');
                        return cacheResponse; // Cache is fresh, return it
                    }
                }

                // Cache is either old or not present, fetch from network
                return fetch(event.request.clone()).then(networkResponse => {
                    // Update the cache with the fresh response
                    cache.put(event.request, networkResponse.clone());

                    // Add custom header to indicate the fetch time
                    const newResponse = new Response(networkResponse.body, networkResponse);
                    newResponse.headers.append('sw-fetch-time', Date.now());
                    console.log('Network loaded.');
                    return newResponse;
                }).catch(() => {
                    // Network request failed, return cached response
                    console.log('Cache loaded.');
                    return cacheResponse;
                });
            });
        })
    );
});


// Update a service worker
self.addEventListener('activate', event => {
    const cacheAllowlist = ['altfuture-cache-v1'];

    event.waitUntil(
        caches.keys().then(cacheNames => {
            return Promise.all(
                cacheNames.map(cacheName => {
                    if (cacheAllowlist.indexOf(cacheName) === -1) {
                        return caches.delete(cacheName);
                    }
                })
            );
        })
    );
});
