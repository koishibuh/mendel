import {defineStore} from 'pinia';
import reviewCreatures from '@/review/data/reviewCreatures.json';
import { ref, computed } from 'vue';
import type { IGrowingCreature } from '@/review/model/IGrowingCreature';

export const useReviewStore = defineStore('reviewStore', () => {

  const creatures = ref<IGrowingCreature[]>(reviewCreatures);

  const getCount = computed(() => {
    return creatures.value.length;
  });

  const getGrowing = computed(() => {
    return creatures.value.filter(creature => creature.age !== 2);
  })

  const getAdults = computed(() => {
    return creatures.value.filter(creature => creature.age !== 2);
  })

  const getFilteredCreatures = (group: string | null) => {
    if (!group || group == "all") return creatures.value;
    if (group === "adults") return creatures.value.filter(creature => creature.age === 2);
    return creatures.value.filter(creature => creature.age !== 2);
  }

  return {
    creatures,
    getCount,
    getGrowing,
    getAdults,
    getFilteredCreatures
  }
})