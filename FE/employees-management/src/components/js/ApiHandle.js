import axios from "axios";
import MResource from "../js/StringResource"
import { checkAuthentication } from "./TokenHandle";
import { store } from "@/main";
import { layoutRouter } from "@/main";

/**
    * Handle err base error object and error type
    * @param {*} error - error object from misa / browser
    * @param {*} type - misa type / browser type
    * @param {*} emitter - form lib to call to event to show dialog err
    * created by: Nguyễn Thiện Thắng
    * created at: 2024/1/20
    */
export function handleErr(error, type, emitter) {
    if (type === MResource.ErrorType.misa) {
        let errMsg = error.data.UserMsg;
        switch (error.data.Code) {
            case 400:
                emitter.emit(
                    MResource.Event.TogleDialog,
                    [errMsg],
                    MResource.DialogType.warning,
                    MResource.DialogIcon.warning,
                );
                break;
            case 401:
                store.commit('changeAuthenticateStatus', false);
                layoutRouter.push("/login");
                break;
            case 403:
                emitter.emit(
                    MResource.Event.TogleDialog,
                    [MResource.DialogMsg.authorizeWarning],
                    MResource.DialogType.warning,
                    MResource.DialogIcon.warning,
                );
                break;
            case 404:
                emitter.emit(
                    MResource.Event.TogleDialog,
                    [errMsg],
                    MResource.DialogType.warning,
                    MResource.DialogIcon.warning,
                );
                break;
            default:
                break;
        }
        // err form browser
    } else {
        //get browser's error message
        let errMsg;
        if (error.response && error.response.data && error.response.data.UserMsg) {
            errMsg = error.response.data.UserMsg;
        } else {
            errMsg = error.message;
        }
        if (error?.response?.status) {
            switch (error.response.status) {
                case 400:
                    emitter.emit(
                        MResource.Event.TogleDialog,
                        [errMsg],
                        MResource.DialogType.warning,
                        MResource.DialogIcon.warning,
                    );
                    break;
                case 401:
                    store.commit('changeAuthenticateStatus', false);
                    layoutRouter.push("/login");
                    break;
                case 403:
                    emitter.emit(
                        MResource.Event.TogleDialog,
                        [MResource.DialogMsg.authorizeWarning],
                        MResource.DialogType.warning,
                        MResource.DialogIcon.warning,
                    );
                    break;
                case 404:
                    emitter.emit(
                        MResource.Event.TogleDialog,
                        [errMsg],
                        MResource.DialogType.warning,
                        MResource.DialogIcon.warning,
                    );
                    break;
                default:
                    emitter.emit(
                        MResource.Event.TogleToast,
                        MResource.ToastStatus.failed,
                        errMsg,
                        true
                    );
                    break;
            }
        } else {
            emitter.emit(
                MResource.Event.TogleToast,
                MResource.ToastStatus.failed,
                MResource.ToastContent.undefinedErr,
                true
            );
        }
    }
}

/**
    * call api to get data
    * @param {*} method - get|post|put|delete
    * @param {*} apiUrl - api url
    * @param {*} data - data to send to api
    * @param {*} type - header content type
    * @param {*} emitter - to operate with error message
    * @param {*} token - access token
    * created by: Nguyễn Thiện Thắng
    * created at: 2024/1/20
    */
export async function apiHandle(method, apiUrl, data, type, emitter) {
    try {
        await checkAuthentication(emitter);
        var token = localStorage.getItem(MResource.Token.AccessToken);
        emitter.emit(MResource.Event.TogleLoading, true);
        switch (method) {
            case MResource.apiMethod.get: {
                const apiResponse = await axios.get(apiUrl,
                    {
                        headers: {
                            Authorization: `Bearer ${token}`
                        }
                    }
                );
                emitter.emit(MResource.Event.TogleLoading, false);
                if (!apiResponse.data.Success) {
                    handleErr(apiResponse, MResource.ErrorType.misa, emitter);
                } else {
                    return apiResponse
                }
                break;
            }
            case MResource.apiMethod.post:
                {
                    const apiResponse = await axios.post(
                        apiUrl,
                        data,
                        {
                            headers: {
                                "Content-Type": type,
                                Authorization: `Bearer ${token}`
                            },
                        }
                    );
                    emitter.emit(MResource.Event.TogleLoading, false);
                    if (!apiResponse.data.Success) {
                        handleErr(apiResponse, MResource.ErrorType.misa, emitter);
                    } else {
                        return apiResponse
                    }
                    break;
                }
            case MResource.apiMethod.put: {
                const apiResponse = await axios.put(apiUrl,
                    data, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                });
                emitter.emit(MResource.Event.TogleLoading, false);
                if (!apiResponse.data.Success) {
                    handleErr(apiResponse, MResource.ErrorType.misa, emitter);
                } else {
                    return apiResponse
                }
                break;
            }
            case MResource.apiMethod.delete: {
                const apiResponse = await axios.delete(
                    apiUrl,
                    {
                        headers: {
                            "Content-Type": type,
                            Authorization: `Bearer ${token}`
                        },
                        data: data
                    }
                );
                emitter.emit(MResource.Event.TogleLoading, false);
                if (!apiResponse.data.Success) {
                    handleErr(apiResponse, MResource.ErrorType.misa, emitter);
                } else {
                    return apiResponse
                }
                break;
            }
            default:
                break;
        }
    } catch (error) {
        emitter.emit(MResource.Event.TogleLoading, false);
        handleErr(error, MResource.ErrorType.browser, emitter);
    }
}


/**
    * api action with Files
    * @param {*} method - get|post|put|delete
    * @param {*} apiUrl - api url
    * @param {*} data - data to send to api
    * @param {*} emitter - to operate with error message
    * @param {*} token - access token
    * created by: Nguyễn Thiện Thắng
    * created at: 2024/3/21
    */
export async function apiFileHandle(method, apiUrl, emitter, data, token) {
    try {
        await checkAuthentication(emitter);
        emitter.emit(MResource.Event.TogleLoading, true);
        switch (method) {
            case MResource.apiMethod.get: {
                // get file form api
                const response = await axios.get(
                    apiUrl,
                    {
                        responseType: MResource.apiHeaderContentType.arrayType,
                        headers: {
                            Authorization: `Bearer ${token}`,
                        },
                    }
                );
                emitter.emit(MResource.Event.TogleLoading, false);
                return response;
            }
            case MResource.apiMethod.post: {
                const response = await axios.post(
                    apiUrl,
                    data,
                    {
                        responseType: MResource.apiHeaderContentType.arrayType,
                        headers: {
                            "Content-Type": MResource.apiHeaderContentType.applicationType,
                            Authorization: `Bearer ${token}`,
                        },
                    }
                );
                emitter.emit(MResource.Event.TogleLoading, false);
                if (!response.data.Success) {
                    handleErr(response, MResource.ErrorType.misa, emitter);
                } else {
                    return response
                }
                break;
            }

            default:
                break;
        }
    } catch (error) {
        emitter.emit(MResource.Event.TogleLoading, false);
        handleErr(error, MResource.ErrorType.browser, emitter);
    }
}
