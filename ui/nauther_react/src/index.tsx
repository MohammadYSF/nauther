import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import { CacheProvider } from '@emotion/react';
import createCache from '@emotion/cache';
import rtlPlugin from 'stylis-plugin-rtl';

const cacheRtl = createCache({
  key: 'muirtl',
  stylisPlugins: [rtlPlugin],
});

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);
root.render(
  <React.StrictMode>
    <CacheProvider value={cacheRtl}>
      <App />
    </CacheProvider>
  </React.StrictMode>
);