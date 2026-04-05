import axios from "axios";
import type{
    CreateHardshipApplicationRequest,
    UpdateHardshipApplicationRequest,
    HardshipApplicationResponse
} from '../types/hardship'

const api = axios.create({
    baseURL:"http://localhost:5000/api",
});

export const createApplication = async (data: CreateHardshipApplicationRequest): Promise<HardshipApplicationResponse> => {
    const response = await api.post('/hardshipapplications', data);
    return response.data;
};

export const getApplicationById = async (id: string): Promise<HardshipApplicationResponse> => {
    const response = await api.get(`/hardshipapplications/${id}`);
    return response.data;
}

export const getApplications = async () : Promise<HardshipApplicationResponse[]> => {
    const response = await api.get('/hardshipapplications');
    return response.data;
}

export const updateApplication = async (id: string, data: UpdateHardshipApplicationRequest): Promise<HardshipApplicationResponse> => {
    const response = await api.put(`/hardshipapplications/${id}`, data);
    return response.data;
}

export const deleteApplication = async (id: string): Promise<void> => {
    await api.delete(`/hardshipapplications/${id}`);
}
