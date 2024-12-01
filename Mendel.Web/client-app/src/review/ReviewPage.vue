<script setup lang="ts">
import { ref, computed, onMounted, reactive, watch } from 'vue';
import { useReviewStore } from '@/review/review.store';
import type { IGrowingCreature } from '@/review/model/IGrowingCreature';
import ToggleButton from '@/components/toggle-button/ToggleButton.vue';
import { useGlobalStore } from '@/common/global.store';

onMounted(() => {
  setSelectedCreature();
  setStep();
  resetTranslate();
});

const store = useReviewStore();
const globalStore = useGlobalStore();

const filterType = ref<string>('all');

const filteredCreatures = ref<IGrowingCreature[]>(store.creatures);

const currentCreatureIndex = ref<number>(1);
const selectedCreature = ref<IGrowingCreature | null>(null);

watch(filterType, (newFilter) => {
  switch (newFilter) {
    case 'adults':
      filteredCreatures.value = store.creatures.filter((creature) => creature.age == 2);
      break;
    case 'growing':
      filteredCreatures.value = store.creatures.filter((creature) => creature.age !== 2);
      break;
    default:
      filteredCreatures.value = store.creatures;
      break;
  }

  setSelectedCreature();
});


const setSelectedCreature = () => {
  selectedCreature.value = filteredCreatures.value[currentCreatureIndex.value];
};

const strip = ref<HTMLDivElement | null>(null);
const transitioning = ref(false);

const setStep = () => {
  if (!strip.value) return;

  const innerWidth = strip.value?.scrollWidth;
  return `${innerWidth / filteredCreatures.value.length}px`;
};

const step = '';

const innerStyle = reactive({
  transform: '',
  transition: ''
});

const next = () => {
  if (transitioning.value) return;
  transitioning.value = true;

  if (!selectedCreature.value) return;

  store.markAsViewed(selectedCreature.value.creatureCode);

  moveLeft();

  const card = filteredCreatures.value.shift();
  if (!card) return;

  filteredCreatures.value.push(card);
  resetTranslate();
  setSelectedCreature();

  transitioning.value = false;
};

const prev = () => {
  if (transitioning.value) return;
  transitioning.value = true;

  if (!selectedCreature.value) return;

  store.markAsViewed(selectedCreature.value.creatureCode);

  innerStyle.transition = 'transform 0.5s ease-in-out';
  moveRight();

  const card = filteredCreatures.value.pop();
  if (!card) return;
  filteredCreatures.value.unshift(card);
  resetTranslate();
  setSelectedCreature();
  transitioning.value = false;
};

const moveLeft = () => {
  innerStyle.transform = `translateX(-${step})`;
  innerStyle.transform = `translateX(-${step})`;
  innerStyle.transition = '0.5s ease-in-out';
};

const moveRight = () => {
  innerStyle.transform = `translateX(${step})`;
  innerStyle.transform = `translateX(-${step})`;
  innerStyle.transition = '0.5s ease-in-out';
};

const resetTranslate = () => {
  innerStyle.transition = 'none';
  innerStyle.transform = 'translateX(-${step})';
};

const pageUrl = computed(() => {
  return `https://finaloutpost.net/view/${selectedCreature.value?.creatureCode}#main`;
});

const goalUrl = computed(() => {
  const baseUrl = 'https://finaloutpost.net/world/species-notes/view';
  const species = selectedCreature.value!.species.replace(/\s+/g, '-');
  const genes = selectedCreature.value!.genes.replace(/\s+/g, '');
  const gender = selectedCreature.value!.gender;
  return `${baseUrl}/${species}/analysis/${genes}/${gender}/`;
});

const openGoalPage = () => {
  if (selectedCreature.value?.age !== 2) {
    // show pop up that creature genes are not unlocked
  } else {
    window.open(goalUrl.value);
  }
};

const progress = computed(() => {
  return (store.viewedCount / store.creatures.length) * 100;
});

const calculatePercentage = computed(() => {
  return `${100 - progress.value}%`;
});

const barColor = computed(() => {
  const intensity = 0.6;
  const green = Math.floor((progress.value / 100) * 255 * intensity);
  const red = Math.floor((255 - green) * intensity);
  return `rgb(${red}, ${green}, 0)`;
});

const showPulsate = ref(false);

watch(progress, (newValue) => {
  if (newValue >= 100) {
    showPulsate.value = true;

    setTimeout(() => {
      showPulsate.value = false;
    }, 3000);
  }
});

const updateFilter = (filter: string) => {
  filterType.value = filter;
};

</script>

<template>
    <div class="p-2">Description of click page.
    </div>
<!--    <div class="flex flex-col gap-2 w-1/3">-->
<!--      <button class="button ">Your Submissions</button>-->
<!--    </div>-->

    <div class="p-2">
      <div class="text-center bg-[#bebebe]">
        Researched {{ store.viewedCount }} / {{ store.creatures.length }}
        Creatures
      </div>
      <div id="progress-bar-box" class="relative m-auto w-full">
        <div id="progress" class="w-full h-5" :style="{ backgroundColor: barColor }"></div>
        <div
          id="progress-bar"
          class="h-5 bg-black absolute top-0 right-0"
          :style="{ width: `${calculatePercentage}` }"
        ></div>
        <div v-if="showPulsate" class="pulsate" ref="pulsateEffect">🌟</div>
      </div>
    </div>

    <ToggleButton
      id="creatureFilter"
      option1-name="growing"
      option2-name="adults"
      option3-name="all"
      @updateState="updateFilter"
       class="px-2"
    />

    <div id="tfoWindow" class="grow mx-5 lg:mx-0">
      <iframe :src="pageUrl" class="w-full h-full" scrolling="no"></iframe>
    </div>

    <div class="flex flex-col gap-2">
      <div class="flex gap-2 justify-center">
        <button
          v-if="selectedCreature?.genes == 'Unknown'"
          class="button w-1/2"
          aria-label="Open example webpage"
          @click="openGoalPage"
          disabled
        >
          💾 Gene Unavailable
        </button>

        <button v-else class="button w-1/2" aria-label="Open example webpage" @click="openGoalPage">
          💾 Save Gene Goal
        </button>

<!--        <button class="button w-1/2" disabled>❤️</button>-->
      </div>

      <div
        class="bg-lightgreen py-2 flex min-h-[95px] min-w-[266px] justify-between self-center w-full"
      >
        <button
          class="bg-transparent flex justify-end items-center w-full"
          @click="prev"
          aria-label="Previous Creature"
          type="button"
        >
          <span
            class="text-offWhite text-end w-[145px] me-1 px-4 py-2 bg-darkbrown hover:bg-sagegreen -scale-x-100 text-2xl"
            >➤</span
          >
        </button>
        <div class="relative w-[266px] h-[95px]">
          <div
            class="absolute overflow-hidden w-[266px] h-[95px] left-1/2 -translate-x-1/2 pointer-events-none opacity-100"
          >
            <div class="inner inline-flex gap-2 items-center" :class="innerStyle" ref="strip">
              <div
                class="card p-2 flex items-center justify-center"
                v-for="(creature, index) in filteredCreatures"
                :key="index"
                :style="{
                  backgroundColor: 'white'
                  // backgroundColor: creature.viewed ? '#56a882' : '#eaeaea'
                }"
              >
                <img :src="creature.portrait" class="mx-auto h-[65px] w-[85px] object-scale-down" :alt="creature.creatureCode" />
                <div v-if="creature.viewed" class="absolute bottom-0 left-0">✅</div>
              </div>
            </div>
          </div>
        </div>
        <button
          class="bg-transparent flex items-center w-full"
          @click="next"
          aria-label="Next Creature"
          type="button"
        >
          <span
            class="text-offWhite text-end w-[145px] ms-1 px-4 py-2 bg-darkbrown hover:bg-sagegreen hover:text-offWhite text-2xl"
            >➤</span
          >
        </button>
      </div>
    </div>

</template>

<style>
.card {
  position: relative;
}

.card:nth-child(1)::before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-image: linear-gradient(rgba(0, 0, 0, 0), rgba(0, 0, 0, 0.4));
  box-shadow: rgba(0, 0, 0, 0.5) 3px 3px 1px;
  pointer-events: none;
}

.card:nth-child(1),
.card:nth-child(3) {
  @apply min-h-[80px] min-w-[80px] border-2 border-[#627c63];
}

.card:nth-child(2) {
  @apply min-h-[90px] min-w-[90px] border-4 border-[#627c63];
  box-shadow: rgba(0, 0, 0, 0.5) 0 3px 1px;
}

.card:nth-child(3)::before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-image: linear-gradient(rgba(0, 0, 0, 0), rgba(0, 0, 0, 0.4));
  box-shadow: rgba(0, 0, 0, 0.5) -3px 3px 1px;
  pointer-events: none;
}

.inner {
  transition: transform 0.2s;
}

#progress-bar {
  transition: width 0.5s ease;
}

.pulsate {
  position: absolute;
  top: -2px;
  right: -5px;
  width: 20px;
  height: 20px;
  border-radius: 50%;
  animation: pulse-animation 1s 3 ease-in-out;
}

@keyframes pulse-animation {
  0%,
  100% {
    transform: scale(1);
    opacity: 1;
  }

  50% {
    transform: scale(1.5); /* Scale up */
    opacity: 0.5; /* Fade out */
  }
}
</style>