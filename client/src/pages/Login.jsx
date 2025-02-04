import { useState } from "react"
import { useAuth } from "../context/AuthContext"
import { useNavigate } from "react-router-dom"
import axios from "axios"
const API_URL = 'https://localhost:7007'

const Login = () => {
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")
    const { login } = useAuth()
    const navigate = useNavigate()

    const handleSubmit = async (e) => {
        e.preventDefault()
        try {
            const response = await axios.post(`${API_URL}/Auth/login`, { email, password })
            login(response.data)
            navigate("/")
        } catch (error) {
            console.error("Ошибка входа", error)
        }
    }

    return (
        <div>
            <h2>Вход</h2>
            <form onSubmit={handleSubmit}>
                <input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} required />
                <input type="password" placeholder="Пароль" value={password} onChange={(e) => setPassword(e.target.value)} required />
                <button type="submit">Войти</button>
            </form>
        </div>
    )
}

export default Login
