import axios from "axios";
import { API_PRIVATE_DOMAIN } from '@env';

const api = axios.create({
    baseURL: `https://${API_PRIVATE_DOMAIN}`
});

export default api;
