<script lang="ts" setup>
import { RouterLink } from 'vue-router';
import ChecklistPage from '@/checklist/ChecklistPage.vue';
</script>

<template>
  <div id="title-navbar" class="bg-[#7e7b9c] rounded-t flex justify-between px-2">
    <div class="">Navigation</div>
    <div class="flex items-center gap-1">
      <div class="h-[20px] w-[20px] bg-[#5fd5e6] rounded"></div>
      <div class="h-[20px] w-[20px] bg-[#23a9ba] rounded"></div>
      <div class="h-[20px] w-[20px] bg-[#007f8f] rounded"></div>
    </div>
  </div>
  <div class="bg-[#7e7b9c] px-2 pb-2 rounded-b h-full">
    <div class="bg-[#faf5ff] h-full p-2 flex flex-col">
      <RouterLink :to="{name: 'Review'}" class="button w-full mb-2">Click</RouterLink>
      <RouterLink :to="{name: 'Checklist'}" class="button w-full mb-2">Checklist</RouterLink>
      <div id="windowSize"></div>
    </div>
  </div>
</template>