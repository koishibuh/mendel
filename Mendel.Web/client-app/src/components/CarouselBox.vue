<script setup lang="ts">
import { ref } from 'vue';
import type { IGrowingCreature } from '@/review/model/IGrowingCreature';

const props = defineProps<{
  creatures: IGrowingCreature[];
}>();

const emit = defineEmits<{
  displayPrevious: [string];
  displayNext: [string];
}>();

const currentCreatureIndex = ref<number>(1);
const creatureCount = ref<number | null>(0);

const nextCreature = () => {
if (currentCreatureIndex.value === creatureCount.value) {
  console.log("No more creatures");
  return;
}
currentCreatureIndex.value -= 1;
};

const previousCreature= () => {
  if (currentCreatureIndex.value === creatureCount.value) {
    console.log("No more creatures");
    return;
  }
  currentCreatureIndex.value += 1;
};
</script>

<template>
  <div @click="previousCreature">First</div>
  <div @click="nextCreature">Current</div>
  <div @click="nextCreature">Next</div>
</template>