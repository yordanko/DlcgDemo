import { Button, Form } from "react-bootstrap";
import { useVideogames } from "../../../lib/hooks/useVideogames";
import { useNavigate, useParams } from "react-router";


export default function VideogameForm() {
    const {id} = useParams();
    const {updateVideoGame, createVideoGame, videoGame, isLoadingVideogames} = useVideogames(id);
    const navigate = useNavigate();

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        // Handle form submission logic here
        const formData = new FormData(event.currentTarget);
        const gameData : {[key:string]: File | string} = {};
        formData.forEach((value, key) => {
            gameData[key] = value;
        });

        if (videoGame) {
            gameData.id = videoGame.id; // Include ID if editing an existing game
            await updateVideoGame.mutateAsync(gameData as unknown as VideoGame);
            navigate(`/videogames/${gameData.id}`);
        }
        else {
            await createVideoGame.mutateAsync(gameData as unknown as VideoGame, {
                onSuccess: (id) => {
                    navigate(`/videogames/${id}`); // Redirect to the videogames list after creation
                }
            });
        }
    }
    if (isLoadingVideogames) {
        return <div className="text-center">Loading Videogames...</div>;
    }

    return (
   
        
    <Form onSubmit={handleSubmit} style={{marginTop:'20px', marginLeft:'20px'}}>
        <h1>{videoGame ? 'Edit Videogame Details' : 'Add Videogame Details'}</h1>
        <Form.Group className="mb-4" controlId="title">
            <Form.Label>Game Name</Form.Label>
            <Form.Control type="text" name='title'  placeholder="Enter game name" 
            defaultValue={videoGame?.title} required/>
        </Form.Group>
        <Form.Group className="mb-4" controlId="genre" >
            <Form.Label>Game Genre</Form.Label>
            <Form.Control type="text" name="genre" placeholder="Enter game genre" defaultValue={videoGame?.genre} required/>
        </Form.Group>
        <Form.Group className="mb-4" controlId="releaseDate">
            <Form.Label>Release Date</Form.Label>
            <Form.Control type="date" name="releaseDate" placeholder="Enter release date" defaultValue={videoGame?.releaseDate
                ? new Date(videoGame.releaseDate).toISOString().split('T')[0] : new Date().toISOString().split('T')[0]
            }/>
        </Form.Group>
        <Form.Group className="mb-4" controlId="description">
            <Form.Label>Game Description</Form.Label>
            <Form.Control as="textarea" rows={3} name="description" placeholder="Enter game description" defaultValue={videoGame?.description} required/>
        </Form.Group>
        <Form.Group className="mb-4" controlId="imageUrl">
            <Form.Label>Game Image URL</Form.Label>
            <Form.Control type="text" name="imageUrl" placeholder="Enter image URL" defaultValue={videoGame?.imageUrl} required/>
        </Form.Group>
        <Form.Group className="mb-4" controlId="platform">
            <Form.Label>Game Platforms</Form.Label>
            <Form.Control type="text" name="platform" placeholder="Enter platforms (comma separated)" defaultValue={videoGame?.platform} />
        </Form.Group>
        <Form.Group className="mb-4" controlId="publisher">
            <Form.Label>Publisher</Form.Label>
            <Form.Control type="text" name="publisher" placeholder="Enter trailer URL" defaultValue={videoGame?.publisher} />
        </Form.Group>
        
        <Button type="button" className="btn btn-secondary ms-2" onClick={()=> navigate(videoGame ? `/videogames/${videoGame?.id}`:'/videogames')}>Cancel</Button> 
        <Button type="submit" className="btn btn-primary" style={{marginLeft:'20px'}}
            disabled={updateVideoGame.isPending || createVideoGame.isPending}
        >Submit</Button>  
    </Form>
  )
}