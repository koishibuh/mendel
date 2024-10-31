import { ref } from 'vue';
import { defineStore } from 'pinia';
import { useAxios } from '@/api/http';
import eventCreatureData from '@/checklist/Data/eventCreatureData.json'
import creatureData from '@/checklist/Data/speciesCreatureData.json'
import newCreateData from '@/checklist/Data/newSpeciesCreatureData.json'

export const useChecklistStore = defineStore('checklistStore', () => {
  const axios = useAxios();
  const eventSpecies = ref<ISpecies[]>(eventCreatureData);
  // const creatures = ref<ICreatureList[]>(creatureData);
  // const newCreatures = ref<>(newCreateData);

  const getSpecies = async () => {
    // try {
    //   const result = await axios.get<ISpecies[]>('/api/species', null);
    //   if (result)
    //   {
    //    eventSpecies.value.push(...result);
    //   }
    // } catch (error) {
    //   console.log(error);
    // }
  };

    const getCreatureSpecies = async (species: string) => {};

  return {
    eventSpecies,
    getSpecies
  };
});

export interface IUserSpecies {
  username: string;
  species: string;
}

export interface ISpecies {
  speciesName: string;
  event: string;
  capsuleUrl: string;
  genes: IGenotype[];
  portraits: IPortrait[];
}

export interface IGenotype {
  id: number;
  trait: string;
  alleles: string;
  description: string;
}

export interface IPortrait {
  juvenile: string | null;
  adult: string;
  gender: string;
  geneIds: number[];
}
//
// export interface ICreatureList {
//   speciesName: string;
//   creatures: ICreature[];
// }
//
// export interface ICreature {
//   code: string;
//   name: string;
//   growthLevel: number;
//   stunted: boolean;
//   gender: string;
//   geneIds: number[] | null;
// }
//
// export interface INewSpecies {
//   speciesName: string;
//   event: string;
//   capsuleUrl: string;
//   genes:
// }
//
// export interface INewGenotype {
//
// }