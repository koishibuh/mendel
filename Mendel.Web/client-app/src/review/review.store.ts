import {defineStore} from 'pinia';
import reviewCreatures from '@/review/data/reviewCreatures.json';
import { ref, computed } from 'vue';
import type { IGrowingCreature } from '@/review/model/IGrowingCreature';

export const useReviewStore = defineStore('reviewStore', () => {

  const creatures = ref<IGrowingCreature[]>(reviewCreatures);

  const filteredCreatures = (group: string) => {
    if (group === "all") {
      creatures.value;
    } else if (group === "adults") {
      creatures.value.filter(creature => creature.age === 2)
    }  else {
      creatures.value.filter(creature => creature.age !== 2);
    }
  }

  const markAsViewed = (code: string) => {
  const creature = creatures.value.find((x) => x.creatureCode === code)
    if (creature){
      creature.viewed = true;
    }
  };

  const viewedCount = computed(() => {
    return creatures.value.filter((x) => x.viewed === true).length;
  })

  const getCount = computed(() => {
    return creatures.value.length;
  });


  const getFilteredCreatures = (group: string) => {
    if (group === "all") {
    return creatures.value;
    } else if (group === "adults") {
      return creatures.value.filter(creature => creature.age === 2)
    }  else {
    return creatures.value.filter(creature => creature.age !== 2);
    }
  }



  return {
    creatures,
    markAsViewed,
    getCount,
    getFilteredCreatures,
    filteredCreatures,
    viewedCount
  }
})