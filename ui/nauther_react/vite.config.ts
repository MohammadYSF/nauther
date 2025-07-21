import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import fs from 'fs'
import path from 'path'

// Custom HTTPS redirect middleware plugin
const httpsRedirectPlugin = () => ({
  name: 'vite:https-redirect',
  configureServer(server:any) {
    server.middlewares.use((req:any, res:any, next:any) => {
      if (!req.socket.encrypted) {
        res.writeHead(301, {
          Location: `https://${req.headers.host}${req.url}`,
        })
        res.end()
      } else {
        next()
      }
    })
  },
})

export default defineConfig({
  server: {
    https: {
      key: fs.readFileSync(path.resolve(__dirname, 'key.pem')),
      cert: fs.readFileSync(path.resolve(__dirname, 'cert.pem')),
      passphrase: 'MaxA',
    },
    port: 44303,
  },
  plugins: [
    react(),
    httpsRedirectPlugin(), // ← now valid
  ],
  resolve: {
    preserveSymlinks: true,
  },
})
