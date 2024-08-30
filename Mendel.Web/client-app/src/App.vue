<script setup lang="ts">
import { RouterView } from 'vue-router';


</script>

<template>
  <div class="bg-[#49817b]">
    <div class="max-w-5xl m-auto flex flex-col h-screen">
      <div class="bg-[#c49ecf]">header</div>
      <RouterView />
      <div class="bg-[#c49ecf]">footer</div>
    </div>
  </div>
  <!-- <header>
    <NavBar />
  </header>

  <div class="bg-red-500">

  </div> -->
</template>