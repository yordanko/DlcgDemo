import { createBrowserRouter } from "react-router"
import App from "../layout/App"
import VideogameForm from "../features/form/VideogameForm"
import Dashboard from "../layout/Dashboard"
import GameDetail from "../Components/GameDetail"
import HomePage from "../../features/home/HomePage"


export const router = createBrowserRouter([
    {
        path: '/', element: <App />,
        children: [
            {path: '', element: <HomePage />},
            {path: 'videogames', element: <Dashboard />},
            {path: 'videogames/:id', element: <GameDetail />},
            {path: '/addVideogame', element: <VideogameForm key='create'/>},
            {path: 'manage/:id', element: <VideogameForm />}
        ]
    }
])