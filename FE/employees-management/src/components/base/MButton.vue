<template>
  <button
    :class="{
      'm-normal-button': type === 'normal',
      'm-button': type === 'primary',
      'm-secondary-button': type === 'secondary',
      'm-icon-button': type === 'icon',
      'm-button-disable': isdisable,
      'danger-btn': isDanger,
    }"
  >
    <i v-if="direction === 'left'" :class="getIconClass(icon)"></i>
    <span :class="{ 'm-icon-button-txt': type === 'icon' }">{{ text }}</span>
    <i v-if="direction === 'right'" :class="getIconClass(icon)"></i>
  </button>
</template>

<script>
export default {
  name: "MButton",
  props: {
    text: {
      type: String,
      required: true,
    },
    type: {
      type: String,
      required: true,
      validator: (value) =>
        ["normal", "primary", "secondary", "icon"].includes(value),
    },
    direction: {
      type: String, // only required when type is 'icon'
      validator: (value) => ["left", "right"].includes(value),
      required: false,
    },
    icon: {
      type: String, // only required when type is 'icon'
    },
    action: {
      type: String,
    },
    isdisable: {
      type: Boolean,
    },
    isDanger: {
      type: Boolean,
    },
  },
  methods: {
    /** split class from tring to class's array
     * @param {*} icon icon class
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2023/12/20
     */
    getIconClass(icon) {
      return icon.split(" ");
    },
  },
};
</script>
<style scoped>
/* normal button */
.m-normal-button:hover {
  background-color: #384fd5;
  box-shadow: 0 2px 15px rgba(42, 126, 252, 0.25);
  transition: all 0.5s;
}

/* Primary button */
.m-normal-button {
  padding: 0 16px;
  width: 100%;
  height: 40px;
  border-radius: 4px;
  font-size: 16px;
  background-color: #0073e6;
  font-weight: 600;
  color: #ffff;
  border: none;
  cursor: pointer;
}

/* Primary button */
.m-button {
  padding: 0 16px;
  min-width: 80px;
  height: 36px;
  border-radius: 4px;
  background-color: #50b83c;
  color: #ffff;
  border: none;
  cursor: pointer;
}

.m-button:hover {
  background-color: #57c841;
}

/* Secondary button */
.m-secondary-button {
  min-width: 80px;
  height: 36px;
  border-radius: 4px;
  border: 1px solid #dedede;
  background-color: #fcfcfc;
  color: #000000;
  cursor: pointer;
}

.m-secondary-button:hover {
  background-color: #e5e4e4;
}

/* Icon button */
.m-icon-button {
  height: 36px;
  min-width: 80px;
  padding: 0 12px;
  font-size: 14px;
  display: flex;
  align-items: center;
  justify-content: space-around;
  border-radius: 4px;
  border: 1px solid #dedede;
  background-color: #fcfcfc;
  color: black;
  cursor: pointer;
  outline: none;
}

.m-button-disable {
  opacity: 50%;
  pointer-events: none;
}

.m-icon-button i {
  background: linear-gradient(
    to right,
    #3291ff,
    #a0cdff,
    #3291ff,
    #e2dddd,
    #3291ff,
    #3291ff,
    #3291ff,
    #3291ff
  );
  -webkit-background-clip: text;
  color: transparent;
  font-size: 20px;
}

.danger-btn i {
  color: red;
}

.m-icon-button-txt {
  padding: 0 4px;
}

.m-icon-button:hover {
  background-color: #f1efef;
}
/* end button */
</style>