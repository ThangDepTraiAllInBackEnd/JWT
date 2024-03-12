<template>
  <div class="toastbox">
    <div class="toast-message-container">
      <div class="toast-message">
        <div :class="type"></div>
        <div class="message-text">
          <span
            :class="{
              'message-title': true,
              'message-corlor-success': type === 'success',
              'message-corlor-fail': type === 'failed',
            }"
            >{{ title }}</span
          >
        </div>
        <span class="message-redo"><a href="">Hoàn tác</a></span>
        <span
          class="message-close"
          id="btn-close-toast"
          @click="togleToast(null, null)"
          ><i class="fa-solid fa-x"></i
        ></span>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MToast",
  props: {
    type: {
      type: String,
      required: true,
      validator: (value) => ["success", "failed"].includes(value),
    },
    title: {
      type: String,
      required: true,
    },
    togleToast: {
      type: Function,
      required: true,
    },
  },
  computed: {
    iconClass() {
      let additionalClass;
      if (this.type === "success") {
        additionalClass = "message-corlor-success";
      } else if (this.type === "fail") {
        additionalClass = "message-corlor-fail";
      }
      return ["fa-solid", this.icon, additionalClass];
    },
  },
};
</script>

<style scoped>
/* toast */
.toastbox {
  position: fixed;
  top: 16px;
  right: 16px;
  pointer-events: none;
  z-index: 5;
  opacity: 100;
  pointer-events: all;
}

.success {
  background: url("../assets/img/Sprites.64af8f61.svg") no-repeat -96px -962px;
  width: 32px;
  height: 32px;
}

.failed {
  background: url("../assets/img/Sprites.64af8f61.svg") no-repeat -32px -962px;
  width: 32px;
  height: 32px;
}

.message-title {
  font-weight: 600;
}

.message-corlor-success {
  color: #50b83c;
}

.message-corlor-fail {
  color: #de3618;
}

.toast-message-container {
  height: 50px;
  width: 460px;
  border-radius: 4px;
  display: flex;
  align-items: center;
  border: 0.5px solid rgb(209, 209, 209);
  background-color: aliceblue;
}

.message-text {
  flex: 1;
  margin-left: 16px;
}

.toast-message {
  display: flex;
  align-items: center;
  padding: 0 16px;
  width: 100%;
}

.message-redo a {
  color: black;
  margin-right: 16px;
}

.message-close {
  cursor: pointer;
  font-size: 12px;
  color: #999999;
}
</style>