<script setup lang="ts">
import { ref, computed } from 'vue';
import { useReviewStore } from '@/review/review.store';
import CreatureBox from '@/review/CreatureBox.vue';

const store = useReviewStore();

const scientistName = ref<string>('');

const filterType = ref<string>('all');

const filteredCreatures = computed(() => store.getFilteredCreatures(filterType.value));
</script>

<template>
  <div class="bg-[#ffedd7] grow flex flex-col">
    <div class="m-2">
      <label for="scientist-name">Scientist Name</label>
      <div class="flex gap-2">
        <input id="scientist-name" class="w-full" type="text" v-model="scientistName" />
        <input type="button" value="Submit" />
      </div>
    </div>

    <div class="w-full flex gap-2 p-2">
      <button class="w-full" @click="filterType = 'growing'">Growing</button>
      <button class="w-full" @click="filterType = 'adults'">Adults</button>
      <button class="w-full" @click="filterType = 'all'">All</button>
    </div>

    <div class="rborder m-2 h-1/2">
      <div>{{filteredCreatures.length }}</div>
      {{ filterType}}
    </div>

    <div class="m-2 flex gap-2">
      <button class="w-full">❤️</button>
      <button class="w-full">💾</button>
    </div>


    <div class="bg-white rborder m-2 flex p-2 gap-2">
      <div v-for="(creature, index) in filteredCreatures" :key="index">
        <CreatureBox :creature="creature" />
      </div>
    </div>

    <div class="m-2">
      <div class="h-4 bg-[#b25a7c] rounded"></div>
      <p class="text-center">Completion Bar</p>

      <div id="windowSize"></div>
    </div>
  </div>
</template>