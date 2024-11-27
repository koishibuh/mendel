<script setup lang="ts">
import type { IGrowingCreature } from '@/review/model/IGrowingCreature';

const props = defineProps<{
  creature: IGrowingCreature;
  index: number;
}>();

const emit = defineEmits<{
  creatureClicked: [number];
}>();

const clickedCreature = () => {
  emit('creatureClicked', props.index);
}
</script>

<template>
  <div class="p-2 rounded border-2 h-[100px] w-[100px]" @click="clickedCreature">
<img :src="creature.portrait" :alt="creature.creatureCode">
  </div>
</template>