<script setup lang="ts">
import { useReviewStore } from '@/review/review.store';

const store = useReviewStore();
</script>

<template>
  <div v-for="(creature, index) in store.creatures" :key="index">

  <div class="flex justify-between border-black border-2 p-2 items-center">
    <div>
      <div class="border-black border-2"><img :src="creature.portrait"  class="mx-auto h-[65px] w-[85px] object-scale-down" :alt="creature.creatureCode" ></div>
      <div>{{creature.creatureCode}}</div>
    </div>
    <div>{{creature.favoriteCount}}</div>
    <div>Remove</div>
  </div>
  </div>
</template>