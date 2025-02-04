import { Link } from "react-router-dom"

const BookCard = ({ book }) => {
    return (
        <div style={{ border: "1px solid #ddd", padding: "10px", borderRadius: "5px" }}>
            <h3>{book.title}</h3>
            <p>Жанр: {book.genre}</p>
            <p>{book.isTaken ? "В наличии" : "Нет в наличии"}</p>
            <Link to={`/book/${book.id}`}>Подробнее</Link>
        </div>
    )
}

export default BookCard
