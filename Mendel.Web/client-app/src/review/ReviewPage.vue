<script setup lang="ts">
import { ref, computed, onMounted, reactive } from 'vue';
import { useReviewStore } from '@/review/review.store';
import CreatureBox from '@/review/CreatureBox.vue';
import CarouselBox from '@/components/CarouselBox.vue';
import type { IGrowingCreature } from '@/review/model/IGrowingCreature';

const store = useReviewStore();

const scientistName = ref<string>('');

const filterType = ref<string>('all');

const filteredCreatures = computed(() => store.getFilteredCreatures(filterType.value));
const currentCreatureIndex = ref<IGrowingCreature>(store.creatures[1]);
const countToDisplay = ref<number>(3);

// const displayCreatures = computed(() => {
//   const start = currentCreatureIndex.value;
//   return filteredCreatures.value.slice(start, start + countToDisplay.value);
// });

// const clickedCreature = (index: number) => {
//   if (index > currentCreatureIndex.value) {
//     currentCreatureIndex.value = Math.max(0, currentCreatureIndex.value - 1);
//   } else {
//     currentCreatureIndex.value = Math.max(
//       displayCreatures.value.length - 3,
//       currentCreatureIndex.value + 1
//     );
//   }
// };

const inner = ref<HTMLDivElement | null>(null);
const transitioning = ref(false);

const step = computed(() => {
  if (!inner.value) return;
  const innerWidth = inner.value?.scrollWidth;
  const totalCreatures = store.creatures.length;
  return `${innerWidth / totalCreatures}px`;
});

const innerStyle = reactive({
  transform: '',
  transition: ''
});

const next = () => {
  if (transitioning.value) return;
  transitioning.value = true;

  store.markAsViewed(currentCreatureIndex.value.creatureCode);
  moveLeft();

  const card = store.creatures.shift();
  if (!card) return;
  store.creatures.push(card);
  resetTranslate();
  transitioning.value = false;
};

const moveLeft = () => {
  innerStyle.transform = `translateX(-${step.value})`;
  innerStyle.transition = '0.5s ease-in-out';
};

const moveRight = () => (innerStyle.transform = `translateX(${step.value})`);

const prev = () => {
  if (transitioning.value) return;
  transitioning.value = true;

  innerStyle.transition = 'transform 0.5s ease-in-out';
  moveRight();

  const card = store.creatures.pop();
  if (!card) return;
  store.creatures.unshift(card);
  resetTranslate();
  transitioning.value = false;
};

function resetTranslate() {
  // innerStyle.transition = 'none';
  innerStyle.transform = `translateX(-${step.value})`;
}

const progress = computed(() => {
  return `${(store.viewedCount / store.creatures.length) * 100}%`;
});
</script>

<template>

  <div class="rborder m-2 grow hidden md:block">
    <div>{{ filteredCreatures.length }}</div>
    {{ filterType }}
  </div>

  <div class="">
      <div class="m-2">
        <label for="scientist-name">Scientist Name</label>
        <div class="flex gap-2">
          <input id="scientist-name" class="w-full" type="text" v-model="scientistName" />
          <input type="button" value="Submit" />
        </div>
      </div>

      <div class="w-full flex gap-2 p-2">
        <button class="w-full" @click="filterType = 'growing'">Growing</button>
        <button class="w-full" @click="filterType = 'adults'">Adults</button>
        <button class="w-full" @click="filterType = 'all'">All</button>
      </div>


      <div class="m-2 flex gap-2">
        <button class="w-full">❤️</button>
        <button class="w-full">💾</button>
      </div>


      <div class="carousel overflow-hidden">
        <div class="inner inline-flex gap-1 pl-2" :class="innerStyle" ref="inner">
          <div
            class="card mr-2 p-2 h-[135px] w-[135px] rborder flex bg-[#fffade]"
            v-for="(creature, index) in store.creatures"
            :key="index"
          >
            <img :src="creature.portrait" class="mx-auto max-h-full object-scale-down"/>
          </div>
        </div>
      </div>

      <div class="m-2 flex gap-2">
        <button class="w-full" @click="prev">Previous</button>
        <button class="w-full" @click="next">Next</button>
      </div>

      <div class="m-2">
        <div class="h-4 bg-[#b25a7c] rounded">
          <div class="h-4 rounded bg-[#983f62] w-1/2" :style="{ width: `${progress}`}"></div></div>
        <p class="text-center">Completion Bar</p>
      </div>
  </div>





<!--  MOBILE-->
<!--  <div class="m-2">-->
<!--    <label for="scientist-name">Scientist Name</label>-->
<!--    <div class="flex gap-2">-->
<!--      <input id="scientist-name" class="w-full" type="text" v-model="scientistName" />-->
<!--      <input type="button" value="Submit" />-->
<!--    </div>-->
<!--  </div>-->

<!--  <div class="w-full flex gap-2 p-2">-->
<!--    <button class="w-full" @click="filterType = 'growing'">Growing</button>-->
<!--    <button class="w-full" @click="filterType = 'adults'">Adults</button>-->
<!--    <button class="w-full" @click="filterType = 'all'">All</button>-->
<!--  </div>-->

<!--  <div class="rborder m-2 grow">-->
<!--    <div>{{ filteredCreatures.length }}</div>-->
<!--    {{ filterType }}-->
<!--  </div>-->

<!--      <div class="m-2 flex gap-2">-->
<!--        <button class="w-full">❤️</button>-->
<!--        <button class="w-full">💾</button>-->
<!--      </div>-->


<!--      <div class="carousel overflow-hidden">-->
<!--          <div class="inner inline-flex gap-1 pl-2" :class="innerStyle" ref="inner">-->
<!--            <div-->
<!--              class="card mr-2 p-2 h-[135px] w-[135px] rborder flex bg-[#fffade]"-->
<!--              v-for="(creature, index) in store.creatures"-->
<!--              :key="index"-->
<!--            >-->
<!--              <img :src="creature.portrait" class="mx-auto max-h-full object-scale-down"/>-->
<!--            </div>-->
<!--          </div>-->
<!--      </div>-->

<!--      <div class="m-2 flex gap-2">-->
<!--        <button class="w-full" @click="prev">Previous</button>-->
<!--        <button class="w-full" @click="next">Next</button>-->
<!--      </div>-->

<!--      <div class="m-2">-->
<!--        <div class="h-4 bg-[#b25a7c] rounded">-->
<!--          <div class="h-4 rounded bg-[#983f62] w-1/2" :style="{ width: `${progress}`}"></div></div>-->
<!--        <p class="text-center">Completion Bar</p>-->
<!--      </div>-->
</template>

<style>
.card:nth-child(1),
.card:nth-child(3) {
}

.inner {
  transition: transform 0.2s;
}
</style>