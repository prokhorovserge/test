import { ref } from 'vue'
import axios, { InternalAxiosRequestConfig } from 'axios'
import { AxiosError, AxiosInstance, AxiosResponse } from 'axios'

export const useApi = () => {
    const config = {
        baseURL: 'https://localhost:7023/api/',
        // baseURL: process.env.PUBLIC_ACT_API_URL || '',
        timeout: 1000,
        headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*'
        },
    }

    const loading = ref(false)
    const error = ref<AxiosError | null>(null)

    const axiosInstance: AxiosInstance = axios.create(config)

    /*
    const onRequestFulfilled = (config: InternalAxiosRequestConfig) => {
        loading.value = true
        error.value = null
        return config;
    }

    const onRequestRejected = (requestError: AxiosError) => {
        loading.value = false
        error.value = requestError
        return Promise.reject(requestError);
    }

    const onResponseFulfilled = (responseObject: AxiosResponse) => {
        loading.value = false
        error.value = null
        return responseObject;
    }

    const onResponseRejected = (responseError: AxiosError) => {
        loading.value = false
        error.value = responseError
        return Promise.reject(responseError);
    }

    axiosInstance.interceptors.request.use(onRequestFulfilled, onRequestRejected);
    axiosInstance.interceptors.response.use(onResponseFulfilled, onResponseRejected);
    */

    return {
        axiosInstance,
        loading,
        error,
    }
}
