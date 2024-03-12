import { createApp } from 'vue'
import { createStore } from 'vuex'
import App from './App.vue'
// lib 
import axios from 'axios';
import tinyEmitter from 'tiny-emitter/instance'
import { saveAs } from 'file-saver';

// Vue router
import { createRouter, createWebHistory } from 'vue-router'

//Router option
// employee option
import EmployeeList from './components/layout/main/views/EmployeeList'
import EmployeeCash from './components/layout/main/views/EmployeeCash'
import EmployeeMoney from './components/layout/main/views/EmployeeMoney'
import EmployeeeOther from './components/layout/main/views/EmployeeOther'
// layout option
import TheLogin from './components/layout/TheLogin'
import TheLayout from './components/layout/TheLayout'

const layoutRoutes = [
    { path: '/', redirect: '/login' },
    { path: '/login', component: TheLogin },
    { path: '/employee-layout', redirect: '/employee-list' },
    {
        path: '/employee-layout',
        component: TheLayout,
        children: [
            { path: '/employee-list', component: EmployeeList },
            { path: '/employee-cash', component: EmployeeCash },
            { path: '/employee-money', component: EmployeeMoney },
            { path: '/employee-other', component: EmployeeeOther }
        ]
    },
];

// Create layout router
export const layoutRouter = createRouter({
    history: createWebHistory(),
    routes: layoutRoutes,
    linkActiveClass: "sidebar-option-element-target"
})

// vuex
const store = createStore({
    state() {
        return {
            isAuthenticate: false,
        }
    },
    mutations: {
        changeAuthenticateStatus(state, status) {
            state.isAuthenticate = status
        }
    }
});

//resource
import MResource from './components/js/StringResource';
import Enum from './components/js/Enum';

const app = createApp(App);
app.config.globalProperties.emitter = tinyEmitter;
app.config.globalProperties.api = axios;
app.config.globalProperties.resource = MResource;
app.config.globalProperties.enum = Enum;
app.config.globalProperties.saveAs = saveAs;
app.use(layoutRouter);
app.use(store);
app.mount('#app')
export { store };
export default { app }; 