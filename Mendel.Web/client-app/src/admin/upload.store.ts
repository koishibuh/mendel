import { useAxios } from '@/api/http';
import { ref, watch, computed } from 'vue';
import { defineStore } from 'pinia'
import type { IImage } from '@/admin/admin.store';

export const useUploadStore = defineStore('uploadStore', () => {
  const axios = useAxios();

  const image = ref<IImage>({ speciesName: '', images: [], age: '', sex: '' });

  const uploadImages = async () => {
    try {
      await axios.post('/api/images', image.value);
    } catch (error) {
      console.log((error as Error).message);
    }
  }


  return {
    image,
    uploadImages
  };
});