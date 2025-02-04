import { Link } from "react-router-dom"
import { useAuth } from "../context/AuthContext"

const Navbar = () => {
    const { user, logout } = useAuth()

    return (
        <nav style={{ display: "flex", gap: "15px", padding: "10px", borderBottom: "1px solid #ddd" }}>
            <Link to="/">Главная</Link>
            {user ? (
                <div>
                    <Link to="/user-books">Мои книги</Link>
                    {user.role === "Admin" && <Link to="/admin">Админ</Link>}
                    <button onClick={logout}>Выйти</button>
                </div>
            ) : (
                <div>
                    <Link to="/login">Войти</Link>
                    <Link to="/register">Регистрация</Link>
                </div>
            )}
        </nav>
    )
}

export default Navbar