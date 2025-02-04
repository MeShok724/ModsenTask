import { useState } from "react"
import { useNavigate } from "react-router-dom"
import axios from "axios"
const API_URL = 'https://localhost:7007'

const Register = () => {
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")
    const [name, setName] = useState("")
    const navigate = useNavigate()

    const handleSubmit = async (e) => {
        e.preventDefault()
        try {
            await axios.post("${API_URL}/Auth/register", { name, email, password })
            navigate("/login")
        } catch (error) {
            console.error("Ошибка регистрации", error)
        }
    }

    return (
        <div>
            <h2>Регистрация</h2>
            <form onSubmit={handleSubmit}>
                <input type="text" placeholder="Имя" value={name} onChange={(e) => setName(e.target.value)} required />
                <input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} required />
                <input type="password" placeholder="Пароль" value={password} onChange={(e) => setPassword(e.target.value)} required />
                <button type="submit">Зарегистрироваться</button>
            </form>
        </div>
    )
}

export default Register
