import { useAxios } from '@/api/http';
import { ref, watch, computed } from 'vue';
import { defineStore } from 'pinia';
import type { ISpeciesDetails } from '@/admin/species.store';
import geneImageData from '@/admin/data/geneImageData.json';
import type { INoGeneImage } from '@/admin/admin.store';

export const useImageGeneStore = defineStore('image-gene-store', () => {
  const axios = useAxios();

  const imageGenes = ref<IImageGene[]>([]);

  const selectedSpecies = ref<ISpeciesDetails>();
  const updateSelectedSpecies = (species: ISpeciesDetails) => {
    selectedSpecies.value = species;
  };

  const selectedSpeciesGenes = ref<IImageGene>({
    speciesId: 0,
    speciesName: '',
    images: [],
    genes: []
  });

  const unknownGeneIds = computed(() => {
    if (!selectedSpeciesGenes.value) return [];
    return selectedSpeciesGenes.value.genes
      .filter((gene) => gene.trait.toLowerCase() === 'unknown')
      .map((gene) => gene.id);
  });

  const imagesWithUnknown = computed(() => {
    if (selectedSpecies.value === null || !selectedSpeciesGenes.value) return [];
    return selectedSpeciesGenes.value.images.filter((image) =>
      image.geneIdSets.some((geneSet) =>
        geneSet.some((geneId) => unknownGeneIds.value.includes(geneId))
      )
    );
  });

  const imagesWithoutUnknown = computed(() => {
    if (selectedSpecies.value === null || !selectedSpeciesGenes.value) return [];
    return selectedSpeciesGenes.value.images.filter(
      (image) =>
        !image.geneIdSets.some((geneSet) =>
          geneSet.some((geneId) => unknownGeneIds.value.includes(geneId))
        )
    );
  });

  const getImageGenes = async () => {
    if (selectedSpecies.value) {
      const result = await axios.get<IImageGene>('/api/images', {
        speciesId: selectedSpecies.value.id
      });
      if (result !== null) {
        selectedSpeciesGenes.value = result;
        const index = imageGenes.value.findIndex((x) => x.speciesId === result.speciesId);
        if (index !== -1) {
          imageGenes.value[index] = result;
        } else {
          imageGenes.value.push(result);
        }
      }
    }
  };

  const newSelectedSpeciesGenes = ref<INewImageGene>({
    speciesId: 0,
    speciesName: '',
    images: [],
    genes: []
  });

  const newImageGenes = ref<INewImageGene[]>([]);

  const getImageGenesNEW = async () => {
    if (selectedSpecies.value) {
      const result = await axios.get<INewImageGene>('api/genes/new/test', {
        speciesId: selectedSpecies.value.id
      });
      if (result !== null) {
        newSelectedSpeciesGenes.value = result;
        const index = newImageGenes.value.findIndex((x) => x.speciesId === result.speciesId);
        if (index !== -1) {
          newImageGenes.value[index] = result;
        } else {
          newImageGenes.value.push(result);
        }
      }
    }
  };

  const removeImageFromGene = async (imageId: number, geneIds: number[][]) => {
    try {
      await axios.remove('/api/image-genes', {
        headers: { 'Content-Type': 'application/json' },
        data: { imageId: imageId, geneIds: geneIds }
      });

      // const index = selectedSpeciesGenes.value.images.findIndex((x) => x.geneIdSets === result.speciesId);
      // if (index !== -1) {
      //   imageGenes.value[index] = result;
      // } else {
      //   imageGenes.value.push(result);
      // }

      // selectedSpeciesGenes.value = selectedSpeciesGenes.value.map(obj => ({
      //   ...obj,
      //   images: obj.images.map(img =>
      //     img.juvenileImageId === imageId
      //       ? { ...img, juvenileImageId: null, juvenileUrl: null }
      //       : img
      //   )
      // }));
    } catch (error) {
      console.log((error as Error).message);
    }
  };

  return {
    updateSelectedSpecies,
    selectedSpecies,
    selectedSpeciesGenes,
    imageGenes,
    getImageGenes,
    removeImageFromGene,
    imagesWithUnknown,
    imagesWithoutUnknown,
    getImageGenesNEW,
    newSelectedSpeciesGenes
  };
});

export interface IImageGene {
  speciesId: number;
  speciesName: string;
  genes: IGenotypeVm[];
  images: INewImage[];
}

export interface IGenotypeVm {
  id: number;
  trait: string;
  alleles: string;
  description: string;
}

export interface INewImage {
  sex: string;
  juvenileImageId: number | null;
  juvenileUrl: string | null;
  adultImageId: number | null;
  adultUrl: string | null;
  geneIdSets: number[][];
}

export interface INewImageGene {
  speciesId: number;
  speciesName: string;
  genes: IGenotypeVm[];
  images: IImageDto[];
}

export interface IImageDto {
  id: number;
  geneId: number;
  url: string;
  age: string;
  sex: string;
}