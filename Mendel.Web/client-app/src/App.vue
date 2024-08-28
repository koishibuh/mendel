<script setup lang="ts">
import { RouterView } from 'vue-router';
import NavBar from './components/NavBar.vue';
</script>

<template>
  <div class="bg-g">
    <div class="max-w-5xl m-auto flex flex-col h-screen">
      <div class="bg-b">header</div>
      <div class="bg-r grow">body</div>
      <div class="bg-e">footer</div>
    </div>
  </div>

  <!-- <header>
    <NavBar />
  </header>

  <div class="bg-red-500">
    <RouterView />
  </div> -->
</template>
