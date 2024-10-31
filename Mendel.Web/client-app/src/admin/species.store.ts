import { useAxios } from '@/api/http';
import { ref, watch, computed, onMounted } from 'vue';
import { defineStore } from 'pinia';
import type { IDropdownMenu } from '@/components/dropdown-menu/dropdownmenu-interface';
import type { INoGeneImage } from '@/admin/admin.store';
import eventData from '@/admin/data/event.json';

export const useSpeciesStore = defineStore('species-store', () => {
  const axios = useAxios();

  onMounted(async () => {
    if (!species.value) {
      await getSpecies();
    }
  });

  const options = ref<IDropdownMenu>({
    name: 'speciesNames',
    options: [],
    placeholder: 'Select Species Name',
    disabled: false,
    maxItem: 1000
  });

  const selectedSpecies = ref<ISpeciesDetails>();
  const updateSelectedSpecies = (species: ISpeciesDetails) => {
    selectedSpecies.value = species;
  };

  const species = ref<ISpeciesDetails[]>();

  const getSpecies = async () => {
    try {
      const result = await axios.get<ISpeciesDetails[]>('/api/species', null);
      if (result) {
        species.value = result;
        options.value.options = result;
      }
    } catch (error) {
      console.log((error as Error).message);
    }
  };

  const speciesImages = ref<INoGeneImage[]>();

  const getSpeciesImages = async (id: number) => {
    try {
      console.log(id);
      const result = await axios.get<INoGeneImage[]>('/api/species/images', { id: id });
      if (result) {
        speciesImages.value = result;
      }
    } catch (error) {
      console.log((error as Error).message);
    }
  };

  const deleteImage = async (id: number) => {
    try {
      const result = await axios.remove('/api/image', { params: { id: id } });
      if (result) {
        species.value = result;
        options.value.options = result;
      }
    } catch (error) {
      console.log((error as Error).message);
    }
  };

  const addSpecies = async () => {
    try {
      const result = await axios.post('/api/species', newSpecies.value);
      if (result) {
        newSpecies.value = {
          name: '',
          capsuleImage: null,
          event: null,
          releaseDate: null
        };
      }
    } catch (error) {
      console.log((error as Error).message);
    }
  };

  const eventNames = ref<IDropdownMenu>({
    name: 'eventNames',
    options: eventData,
    placeholder: 'Select Event Name',
    disabled: false,
    maxItem: 1000
  });

  const setEvent = (event: any) => {
    newSpecies.value.event = event.name;
  };

  const newSpecies = ref<ISpeciesDto>({
    name: '',
    capsuleImage: null,
    event: null,
    releaseDate: null
  });

  const newGenes = ref(
    {
      speciesId: 0,
      geneJson: ''
    }
  );

  const addGenes = async () => {
    try {
      const result = await axios.post('/api/genes/speciestest', { speciesId: newGenes.value.speciesId,  geneJson: newGenes.value.geneJson });
      if (result) {
        newSpecies.value = {
          name: '',
          capsuleImage: null,
          event: null,
          releaseDate: null
        };
      }
    } catch (error) {
      console.log((error as Error).message);
    }
  }

  return {
    species,
    options,
    selectedSpecies,
    updateSelectedSpecies,
    getSpecies,
    deleteImage,
    speciesImages,
    getSpeciesImages,
    newSpecies,
    addSpecies,
    setEvent,
    eventNames,
    newGenes,
    addGenes
  };
});

export interface ISpeciesDetails {
  id: number;
  name: string;
}

export interface ISpeciesDto {
  name: string;
  capsuleImage: string | null;
  event: string | null;
  releaseDate: Date | null;
}