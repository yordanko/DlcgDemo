import { createBrowserRouter } from "react-router"
import App from "../layout/App"


export const router = createBrowserRouter([
        {
            path: '/', element: <App />,
            // children: [
            //     {path: '', element: <div>Home Page</div>},
            //     {path: 'videogames', element: <Dashboard />},
            //     {path: '/create', element: <GameForm />},
    }
])