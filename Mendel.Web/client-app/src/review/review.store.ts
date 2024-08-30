import {defineStore} from 'pinia';
import reviewCreatures from '@/review/data/reviewCreatures.json';
import { ref } from 'vue';
import type { IGrowingCreature } from '@/review/model/IGrowingCreature';

export const useReviewStore = defineStore('reviewStore', () => {

  const creatures = ref<IGrowingCreature[]>(reviewCreatures);


  return {
    creatures
  }
})