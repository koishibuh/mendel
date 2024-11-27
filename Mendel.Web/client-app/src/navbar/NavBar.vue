<script lang="ts" setup>
import { RouterLink } from 'vue-router';

const emit = defineEmits(['close-menu']);

const closeMenu = () => {
  emit('close-menu');
};
</script>

<template>
  <div class="p-2 flex flex-col bg-[#ECEEF0]">
    <RouterLink @click="closeMenu" :to="{ name: 'Review' }" class="button w-full mb-2"
      >Click</RouterLink
    >
    <RouterLink @click="closeMenu" :to="{ name: 'Checklist' }" class="button w-full mb-2"
      >Checklist</RouterLink
    >
    <div id="windowSize"></div>
  </div>
</template>