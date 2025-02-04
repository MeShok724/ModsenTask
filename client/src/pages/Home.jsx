import { useEffect, useState } from "react"
import { getBooks } from "../api/books"
import BookCard from "../components/BookCard"

const Home = () => {
    const [books, setBooks] = useState([])

    useEffect(() => {
        getBooks().then(setBooks)
    }, [])

    return (
        <div>
            <h1>Список книг</h1>
            <div style={{ display: "grid", gap: "10px" }}>
                {books.map((book) => (
                    <BookCard key={book.id} book={book} />
                ))}
            </div>
        </div>
    )
}

export default Home