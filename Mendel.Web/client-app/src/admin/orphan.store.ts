import { useAxios } from '@/api/http';
import { ref, watch, computed } from 'vue';
import { defineStore } from 'pinia';
import cloudinaryData from '@/admin/data/cloudinaryData.json';

export const useOrphanStore = defineStore('orphan-store', () => {
  const axios = useAxios();

  const orphanedImages = ref<IOrphanedImage[]>();

  const getOrphanedImages = async (speciesId: number) => {
    try {
      const result = await axios.get<IOrphanedImage[]>('/api/images/orphaned', { speciesId: speciesId });
      if (result) {
        orphanedImages.value = result;
      }
    } catch (error) {
      console.log((error as Error).message);
    }
  };

  // const selectedOrphans = ref<IOrphanedImage[]>([]);
  const selectedOrphans = ref<number[]>([]);

  const assignUnknownGeneToOrphans = async (geneId: number) => {
    try {
      // const imageIds = selectedOrphans.value.map(x => x.id);
      await axios.post('/api/genes/unknown', { geneId: geneId, imageIds: selectedOrphans.value });
      const test = orphanedImages.value?.filter((x) => !selectedOrphans.value.includes(x.id));
      orphanedImages.value = test;
      selectedOrphans.value = [];
    } catch (error) {
      console.log((error as Error).message);
    }
  };

  const cloudinarySearch = ref<string>('');
  const getCloudinaryImages = async () => {
    const result = await axios.post<ICloudinary[]>('api/images/cloudinary', {
      name: cloudinarySearch.value
    });
    if (result) {
      cloudinaryResults.value = result;
    }
  };

  const cloudinaryResults = ref<ICloudinary[]>(cloudinaryData);

  const selectedCloudinaryImages = ref<ICloudinarySpecies>({
    speciesId: 0,
    age: "",
    sex: "",
    images: []
  });

  const saveCloudinaryImages = async () => {
    await axios.post('api/cloudinary/genes', selectedCloudinaryImages.value );
    const test = cloudinaryResults.value?.filter((x) => !selectedCloudinaryImages.value.images.includes(x));
    cloudinaryResults.value = test;
    selectedCloudinaryImages.value = {
      speciesId: 0,
      age: "",
      sex: "",
      images: []
    };
  };

  return {
    getOrphanedImages,
    orphanedImages,
    selectedOrphans,
    assignUnknownGeneToOrphans,
    cloudinarySearch,
    getCloudinaryImages,
    cloudinaryResults,
    selectedCloudinaryImages,
    saveCloudinaryImages
  };
});

export interface IOrphanedImage {
  id: number;
  url: string;
  age: string;
  sex: string;
}

export interface ICloudinary {
  url: string;
  publicId: string;
}

export interface ICloudinarySpecies {
  speciesId: number;
  age: string;
  sex: string;
  images: ICloudinary[];
}