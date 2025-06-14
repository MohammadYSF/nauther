import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import '@fontsource/vazirmatn/400.css';
import '@fontsource/vazirmatn/700.css'; // Defaults to weight 400
import './index.css'


const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);