<template>
  <div class="main-container">
    <router-view></router-view>
  </div>
</template>

<script>
export default {
  name: "TheMain",
};
</script>

<style scoped>
.main-container {
  padding-left: 24px;
  background-color: #f4f5f7;
}
</style>