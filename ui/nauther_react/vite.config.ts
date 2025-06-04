import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import fs from "fs";
import path from "path";
// https://vite.dev/config/
export default defineConfig({
  server: {
    https: {
      key: fs.readFileSync(path.resolve(__dirname, "key.pem")),
      cert: fs.readFileSync(path.resolve(__dirname, "cert.pem")),
      passphrase: "MaxA",
    },
    port: 44303,    
  },
  plugins: [react()],
  resolve: {
    preserveSymlinks: true,
  },
  configureServer(server) {
    server.middlewares.use((req, res, next) => {
      // Check if the request is not HTTPS
      if (!req.socket.encrypted) {
        // Redirect to HTTPS
        res.writeHead(301, {
          Location: `https://${req.headers.host}${req.url}`,
        });
        res.end();
      } else {
        next();
      }
    });
  },
});
