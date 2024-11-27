import { createRouter, createWebHistory } from 'vue-router';
import ReviewPage from '@/review/ReviewPage.vue';
// import ChecklistPage from '@/checklist/ChecklistPage.vue';
// import AdminPage from '@/admin/AdminPage.vue';
// import SpeciesPage from '@/admin/SpeciesPage.vue';
// import OrphanPage from '@/admin/OrphanPage.vue';
// import UploadPage from '@/admin/UploadPage.vue';
// import AssignPage from '@/admin/AssignPage.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Review',
      component: ReviewPage
    },
    // {
    //   path: '/checklist',
    //   name: 'Checklist',
    //   component: ChecklistPage
    // },
    // {
    //   path: '/admin',
    //   name: 'Admin',
    //   component: AdminPage
    // },
    // {
    //   path: '/orphan',
    //   name: 'Orphan',
    //   component: OrphanPage
    // },
    // {
    //   path: '/upload',
    //   name: 'Upload',
    //   component: UploadPage
    // },
    // {
    //   path: '/species',
    //   name: 'Species',
    //   component: SpeciesPage
    // },
    // {
    //   path: '/assign',
    //   name: 'Assign',
    //   component: AssignPage
    // },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue')
    }
  ]
});

export default router;