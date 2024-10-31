<script setup lang="ts">
import { RouterView } from 'vue-router';
import NavBar from '@/navbar/NavBar.vue';
</script>

<template>
  <div id="window" class="flex flex-col lg:grid lg:grid-cols-10 lg:grid-rows-9 h-screen lg:w-11/12 m-auto">
    <div id="main-top" class="lg:col-span-7 lg:row-start-1 lg:content-end xl:col-span-8">
      <div id="title-bar" class="bg-[#1c96b8] lg:rounded-t flex justify-between px-2">
        <div class="">MENDEL.exe</div>
        <div class="flex items-center gap-1">
          <div class="h-[20px] w-[20px] bg-[#00b9c7] rounded"></div>
          <div class="h-[20px] w-[20px] bg-[#3ddac7] rounded"></div>
          <div class="h-[20px] w-[20px] bg-[#86f9bc] rounded"></div>
        </div>
      </div>
    </div>

    <main
      class="lg:col-span-7 lg:row-span-7 lg:row-start-2 grow overflow-x-scroll px-2 bg-[#1c96b8] xl:col-span-8"
    >
      <div class="bg-[#e6f4f1] min-h-[100%] flex flex-col">
      <RouterView />
      </div>
    </main>

    <div id="main-bottom" class="hidden lg:block lg:col-span-7 lg:row-start-9 lg:content-start xl:col-span-8">
      <div class="bg-[#1c96b8] rounded-b">.</div>
    </div>

    <div id="nav-box"
      class="lg:row-span-3 lg:row-start-2 lg:col-span-3 lg:ml-2 xl:col-span-2"
    >
     <NavBar/>
    </div>
  </div>

  <!-- <header>
    <NavBar />
  </header>

  <div class="bg-red-500">

  </div> -->
</template>

<style>
main {
  filter: drop-shadow(-5px 7px 0px #303030);
}
</style>