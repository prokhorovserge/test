import { GetCurrentWeatherQuery, GetCurrentWeatherResult, GetWeatherForecastQuery, GetWeatherForecastResult } from './types'
import { useApi } from './useApi'

export const useWeatherApi = () => {
    const { axiosInstance, loading, error } = useApi()

    const getCurrent = async (param: GetCurrentWeatherQuery): Promise<GetCurrentWeatherResult> =>
        await axiosInstance.request({
            url: 'weather/GetCurrent',
            method: 'get',
            params: param
        }).then(response => response.data)
            .catch(error => { throw error })

    const getForecast = async (param: GetWeatherForecastQuery): Promise<GetWeatherForecastResult> =>
        await axiosInstance.request({
            url: 'weather/GetForecast',
            method: 'get',
            params: param
        }).then(response => response.data)
            .catch(error => { throw error })

    return {
        getCurrent,
        getForecast,
        loading,
        error,
    }
}
