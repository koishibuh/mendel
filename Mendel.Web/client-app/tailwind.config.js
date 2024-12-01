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
        darkbrown: '#713928',
        sagegreen: '#515A4E',
        lightgreen: '#8d9d91',
        jadegreen: '#799b9f',
        darkgray: '#cdced0',
        offWhite: '#eee5d8',
        offBlack: '#1f2224'
      }
    }
  },
  plugins: []
};