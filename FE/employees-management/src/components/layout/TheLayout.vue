<template>
  <div class="layout-container">
    <the-header />
    <the-side-bar />
    <the-main />
  </div>
</template>

<script>
import TheHeader from "./header/TheHeader.vue";
import TheSideBar from "./sidebar/TheSideBar.vue";
import TheMain from "./main/TheMain.vue";
export default {
  components: { TheHeader, TheSideBar, TheMain },
  name: "TheLayout",
};
</script>
<style>
* {
  box-sizing: border-box;
}

.layout-container {
  display: grid;
  grid-template-columns: 200px 1fr;
  grid-template-rows: 56px 1fr;
  grid-template-areas:
    "sidebar header "
    "sidebar main ";
  height: 100vh;
  width: 100vw;
}

.header-container {
  grid-area: header;
}

.sidebar-container {
  grid-area: sidebar;
}

.main-container {
  grid-area: main;
  height: calc(100vh - 56px);
}
</style>