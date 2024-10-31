import { useAxios } from '@/api/http';
import { ref, watch, computed } from 'vue';
import { defineStore } from 'pinia';
import type { IJwt } from '@/admin/models/jwt.interface';
import speciesGenesData from '@/admin/data/speciesGenesData.json';
import speciesPortraitData from '@/admin/data/speciesPortraitData.json';
import portraitGeneData from '@/admin/data/portraitGeneData.json';
import juvenileNoGeneData from '@/admin/data/juvenileNoGeneData.json';
import type { IGenotypeVm, IImageGene } from '@/admin/image-gene.store';

export const useAdminStore = defineStore('admin-store', () => {
  const axios = useAxios();

  const userData = ref<IJwt | null>();



  const imageGenes = ref<IImageGene>({ speciesId: 0, speciesName: '', genes: [], images: [] });

  const filterActive = ref(false);

  const filteredGenes = computed(() => {
    if (filterActive.value) {
      return imageGenes.value.images
        .filter((x) => x.juvenileUrl === null || x.adultUrl === null)

    }
    return imageGenes.value.images;
  });



  const getSpeciesGenes = async (speciesName: string) => {
    try {
      const result = await axios.get<IGenotypeVm[]>('/api/species/genes', {
        speciesName: speciesName
      });
    } catch (error) {
      console.log((error as Error).message);
    }
  };

  const image = ref<IImage>({ speciesName: '', images: [], age: '', sex: '' });

  const submitImage = async () => {
    await axios.post('/api/cloud/image', image.value);
    image.value.images = [];
  };

  const selectedJuvenile = ref<INoGeneImage>();
  const selectedAdults = ref<number[]>([]);
  const geneIdSets = computed(() => {
    return imageGenes.value.images
      // .filter((x) => selectedAdults.value.includes(x.AdultImageId))
      .flatMap((x) => x.geneIdSets);
  });

  const addJuvenileToAdult = async () => {
    await axios.post('/api/juvenile/gene/add', {
      juvenileImageId: selectedJuvenile.value?.id,
      geneIdSets: geneIdSets.value
    });

    imageGenes.value.images.forEach((x) => {
      // if (selectedAdults.value.includes(x.AdultImageId)) {
      //   x.juvenileUrl = selectedJuvenile.value!.url;
      // }
    });

    selectedAdults.value = [];
  };

  const juvenileNoGene = ref<INoGeneImage[]>([]);

  const getJuvenileNoGenes = async () => {
    const result: INoGeneImage[] | null = await axios.post('/api/juvenile/gene', null);
    if (result !== null) {
      juvenileNoGene.value = result;
    }
  };

  //
  // const loginUser = async (username: string, password: string) => {
  //   const result = await axios.post<AxiosResponse>('/api/login', {
  //     username: username,
  //     password: password
  //   });

  //   if (result !== null) {
  //     setUserData(result.data);
  //   }
  // };

  // function setUserData(data: IJwt): void {
  //   userData.value = data;
  //   localStorage.setItem('user', JSON.stringify(data));
  //   axios.setAuthorizationHeader(data.token);
  // }
  //
  // function getUserData(): void {
  //   const data: any = localStorage.getItem('user');
  //   if (!data) {
  //     return;
  //   }
  //   const parsed = JSON.parse(data);
  //   console.log(parsed);
  //   axios.setAuthorizationHeader(parsed.token);
  // }

  return {


    getSpeciesGenes,
    submitImage,
    image,
    juvenileNoGene,
    selectedJuvenile,
    selectedAdults,
    addJuvenileToAdult,
    geneIdSets,
    filteredGenes,

    getJuvenileNoGenes,
    filterActive

    // loginUser,
    // getUserData
  };
});



export interface IImage {
  speciesName: string;
  images: any[];
  age: string;
  sex: string;
}

export interface INoGeneImage {
  id: number;
  url: string;
  age: string;
  sex: string;
}