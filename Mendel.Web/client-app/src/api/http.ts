import axios from 'axios';

export const useAxios = () => {

  const get = async <T> (url: string, data: any | null) : Promise<T | null> => {
    try {
      const response = await axios.get(url, { params: data });
      console.log("axios", response);
      return response.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        const message = error.response?.data?.status + ': ' + error.response?.data?.title;
        console.log(message);
      }
      return null;
    }
  }

  const post = async <T> (url: string, data: any | null) : Promise<T | null> => {
    try {
      const response = await axios.post(url, data);
      return response.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        const message = error.response?.data?.status + ': ' + error.response?.data?.title + ' - ' + error.response?.data.errors[0];
        console.log(message);
      }
      return null;
    }
  };

  const patch = async (url: string, data: any | null) => {
    try {
      const response = await axios.patch(url, data);
      return response.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        const message = error.response?.data?.status + ': ' + error.response?.data?.title;
        console.log(message);
      }
    }
  }

  const remove = async (url: string, data: any | null) => {
    try {
      const response = await axios.delete(url, data);
      return response.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        const message = error.response?.data?.status + ': ' + error.response?.data?.title;
        console.log(message);
      }
    }
  }

  return {
    get,
    post,
    patch,
    remove
  };
}