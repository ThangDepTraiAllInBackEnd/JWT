<template>
  <the-page></the-page>
</template>

<script>
import ThePage from "./components/layout/ThePage.vue";

export default {
  name: "App",
  components: {
    ThePage,
  },
};
</script>

<style>
@font-face {
  font-family: hihi;
  src: url("./components/assets/fonts/notosans-regular.2cb88a13.woff2");
}
body {
  font-family: hihi;
  margin: 0;
  font-size: 13px;
}
</style>
