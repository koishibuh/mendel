/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors: {
        g: '#378e4d',
        r: '#c13030',
        b: '#4f82bc',
        e: '#60378e',
        sagegreen: '#515A4E',
        lightgreen: '#b2c0a7',
        darkbrown: '#713928',
        brown: '#b49484',
        lightbrown: '#dea987',
        darkgray: '#cdced0'
      }
    }
  },
  plugins: []
};