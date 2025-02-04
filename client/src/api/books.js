import axios from "axios"
const API_URL = 'https://localhost:7007'

// �������� ������ ����
export const getBooks = async (query = "") => {
    const response = await axios.get(`${API_URL}/Book?search=${query}`)
    console.log(response);
    return response.data
}

// �������� ���������� � �����
export const getBookById = async (id) => {
    const response = await axios.get(`${API_URL}/Book/${id}`)
    return response.data
}

// �������� ����� ����� (������ ��� ������)
export const addBook = async (bookRequest) => {
    const response = await axios.post(`${API_URL}/Book`, bookRequest)
    return response.data
}

// ������� ����� (������ ��� ������)
export const deleteBook = async (id) => {
    const response = await axios.delete(`${API_URL}/Book/${id}`)
    return response.data
}
