import { defineConfig } from 'astro/config';
import tailwind from '@astrojs/tailwind';
import node from '@astrojs/node';

export default defineConfig({
  output: 'server',
  adapter: node({
    mode: 'standalone',
  }),
  integrations: [tailwind({ configFile: './tailwind.config.mjs' })],
  server: {
    port: 4321,
  },
});
