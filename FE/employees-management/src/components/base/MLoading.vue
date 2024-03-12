<template>
  <div id="loading-bar-spinner" class="spinner">
    <div class="spinner-icon"></div>
  </div>
</template>
<script>
export default {
  name: "MLoading",
};
</script>


<style scoped>
#loading-bar-spinner.spinner {
  left: 50%;
  margin-left: -20px;
  top: 50%;
  margin-top: -20px;
  position: fixed;
  z-index: 19 !important;
  -webkit-animation: loading-bar-spinner 400ms linear infinite;
  animation: loading-bar-spinner 400ms linear infinite;
  pointer-events: none;
}

#loading-bar-spinner.spinner .spinner-icon {
  width: 40px;
  height: 40px;
  border: solid 4px transparent;
  border-bottom-color: transparent !important;
  border-radius: 50%;
  -webkit-animation: colors 5s infinite;
  animation: colors 5s infinite;
}

@-webkit-keyframes loading-bar-spinner {
  0% {
    -webkit-transform: rotate(0deg);
    transform: rotate(0deg);
    transform: rotate(0deg);
  }

  100% {
    -webkit-transform: rotate(360deg);
    transform: rotate(360deg);
    transform: rotate(360deg);
  }
}

@keyframes loading-bar-spinner {
  0% {
    -webkit-transform: rotate(0deg);
    transform: rotate(0deg);
    transform: rotate(0deg);
  }

  100% {
    -webkit-transform: rotate(360deg);
    transform: rotate(360deg);
    transform: rotate(360deg);
  }
}

@keyframes colors {
  0% {
    border-color: #1b9a59;
  }

  25% {
    border-color: #1b9a59;
  }

  50% {
    border-color: #1b9a59;
  }

  75% {
    border-color: #1b9a59;
  }

  100% {
    border-color: #1b9a59;
  }
}
</style>