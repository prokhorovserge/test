import { ref } from 'vue'
import { GetCurrentWeatherQuery, GetCurrentWeatherResult, GetWeatherForecastQuery, GetWeatherForecastResult } from './types'
import { useApi } from './useApi'

export const useWeatherApi = () => {
    const { axiosInstance } = useApi()
    const currentLoading = ref(false)
    const currentData = ref<GetCurrentWeatherResult | null>(null)
    const currentError = ref(null)
    const forecastLoading = ref(false)
    const forecastData = ref<GetWeatherForecastResult | null>(null)
    const forecastError = ref(null)

    const getCurrent = async (param: GetCurrentWeatherQuery): Promise<GetCurrentWeatherResult> =>
        axiosInstance.request({
            url: 'weather/GetCurrent',
            method: 'GET',
            params: param
        }).then(response => {
                currentData.value = response.data
                currentLoading.value = false
                currentError.value = null
                return response.data
            })
            .catch(error => {
                currentData.value = null
                currentLoading.value = false
                currentError.value = error
                return error.response?.data
             })

    const getForecast = async (param: GetWeatherForecastQuery): Promise<GetWeatherForecastResult> =>
        axiosInstance.request({
            url: 'weather/GetForecast',
            method: 'GET',
            params: param
        }).then(response => {
                forecastData.value = response.data
                console.log('getForecast', forecastData.value)
                forecastLoading.value = false
                forecastError.value = null
                return response.data
            })
            .catch(error => {
                forecastData.value = null
                forecastLoading.value = false
                forecastError.value = error
                return error.response?.data
             })

    return {
        getCurrent,
        getForecast,
        currentLoading,
        currentData,
        currentError,
        forecastLoading,
        forecastData,
        forecastError,
    }
}
