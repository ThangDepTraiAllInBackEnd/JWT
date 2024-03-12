<template>
  <div id="dialog-container">
    <div class="dialog-content">
      <div class="dialog-top">
        <div :class="dialogIcon"></div>
        <div class="top-msg">
          <li v-for="(message, index) in msgs" :key="index">{{ message }}</li>
        </div>
      </div>
      <div
        v-if="dialogType === 'dataChangeDialog'"
        class="dialog-bottom-confirm-change"
      >
        <m-button type="secondary" text="Hủy" @click="togleDialog" />
        <div class="bottom-right">
          <m-button
            type="secondary"
            text="Không"
            class="say-no-btn"
            @click="sayNoAndAction"
          />
          <m-button type="primary" text="Có" @click="sayOkAndAction" />
        </div>
      </div>

      <div v-if="dialogType === 'errDialog'" class="dialog-bottom-err">
        <m-button type="primary" text="Đóng" @click="togleDialog" />
      </div>

      <div v-if="dialogType === 'warningDialog'" class="dialog-bottom-warning">
        <m-button type="primary" text="Đồng ý" @click="togleDialog" />
      </div>

      <div
        v-if="dialogType === 'deleteConfirmDialog'"
        class="dialog-bottom-confirm-change"
      >
        <m-button type="secondary" text="Không" @click="togleDialog" />
        <m-button type="primary" text="Có" @click="confirmSayYes()" />
      </div>
    </div>
  </div>
</template>

<script>
import MButton from "./MButton.vue";
export default {
  components: { MButton },
  name: "MDialog",
  props: {
    msgs: {
      type: Array,
      required: true,
    },
    togleDialog: {
      type: Function,
      required: true,
    },
    dialogType: {
      type: String,
      required: true,
    },
    dialogIcon: {
      type: String,
      required: true,
    },
  },
  methods: {
    sayOkAndAction() {
      this.$emit("confirmSave");
      this.togleDialog();
    },

    sayNoAndAction() {
      this.$emit("confirmNotSave");
      this.togleDialog();
    },
    confirmSayYes() {
      this.$emit("confirmSayYes");
      this.togleDialog();
    },
  },
};
</script>

<style scoped>
#dialog-container {
  opacity: 100%;
  pointer-events: all;
  width: 100%;
  height: 100vh;
  display: flex;
  justify-content: center;
  position: fixed;
  top: 0;
  left: 50%;
  transform: translateX(-50%);
  z-index: 5;
  background: rgb(0, 0, 0, 0.5);
}

.dialog-content {
  width: 25%;
  position: absolute;
  top: 30%;
  padding: 24px;
  border-radius: 4px;
  background-color: #ffff;
}

.dialog-top {
  display: flex;
  align-items: center;
  padding-bottom: 24px;
  border-bottom: 1px solid #e6e6e6;
}

.dialog-top li {
  list-style-type: none;
}
.warning {
  background: url("../assets/img/Sprites.64af8f61.svg") no-repeat -598px -463px;
  width: 54px;
  height: 37px;
  margin-right: 12px;
}

.not-valid {
  background: url("../assets/img/Sprites.64af8f61.svg") no-repeat -752px -462px;
  width: 43px;
  height: 37px;
  margin-right: 12px;
}
.confirm {
  background: url("../assets/img/Sprites.64af8f61.svg") no-repeat -832px -462px;
  width: 43px;
  height: 37px;
  margin-right: 12px;
}

.dialog-bottom-err {
  width: 100%;
  display: flex;
  margin-top: 24px;
  justify-content: center;
}

.dialog-bottom-warning {
  width: 100%;
  display: flex;
  margin-top: 24px;
  justify-content: end;
}

.dialog-bottom-confirm-change {
  width: 100%;
  display: flex;
  margin-top: 24px;
  justify-content: space-between;
}
.say-no-btn {
  margin-right: 8px;
}
</style>