import { Button, Form } from "react-bootstrap";
import { useVideogames } from "../../../lib/hooks/useVideogames";
type Props = {
    videoGame?: VideoGame;
    closeForm: () => void;
}

export default function VideogameForm({videoGame, closeForm}: Props) {
    const {updateVideoGame, createVideoGame} = useVideogames();
    
    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        // Handle form submission logic here
        const formData = new FormData(event.currentTarget);

        console.log("Form Data:", formData);
        const gameData : {[key:string]: File | string} = {};
        formData.forEach((value, key) => {
            gameData[key] = value;
        });

        console.log("Game Data:", gameData);
        if (videoGame) {
            console.log("Updating existing game with ID:", videoGame.id);
            gameData.id = videoGame.id; // Include ID if editing an existing game
            await updateVideoGame.mutateAsync(gameData as unknown as VideoGame);
            closeForm();
        }
        else {
            await createVideoGame.mutateAsync(gameData as unknown as VideoGame);
            closeForm();
        }
    }
  return (
    <>
        <div>Edit Videogame Details</div>
        <Form onSubmit={handleSubmit} className="mt-3">
            
        <Form.Group className="mb-3" controlId="title">
            <Form.Label>Game Name</Form.Label>
            <Form.Control type="text" name='title'  placeholder="Enter game name" 
            defaultValue={videoGame?.title} required/>
        </Form.Group>
        <Form.Group className="mb-3" controlId="genre" >
            <Form.Label>Game Genre</Form.Label>
            <Form.Control type="text" name="genre" placeholder="Enter game genre" defaultValue={videoGame?.genre} required/>
        </Form.Group>
        <Form.Group className="mb-3" controlId="releaseDate">
            <Form.Label>Release Date</Form.Label>
            <Form.Control type="date" name="releaseDate" placeholder="Enter release date" defaultValue={videoGame?.releaseDate
                ? new Date(videoGame.releaseDate).toISOString().split('T')[0] : new Date().toISOString().split('T')[0]
            }/>
        </Form.Group>
        <Form.Group className="mb-3" controlId="description">
            <Form.Label>Game Description</Form.Label>
            <Form.Control as="textarea" rows={3} name="description" placeholder="Enter game description" defaultValue={videoGame?.description} required/>
        </Form.Group>
        <Form.Group className="mb-3" controlId="imageUrl">
            <Form.Label>Game Image URL</Form.Label>
            <Form.Control type="text" name="imageUrl" placeholder="Enter image URL" defaultValue={videoGame?.imageUrl} required/>
        </Form.Group>
        <Form.Group className="mb-3" controlId="publisher">
            <Form.Label>Publisher</Form.Label>
            <Form.Control type="text" name="publisher" placeholder="Enter trailer URL" defaultValue={videoGame?.publisher} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="platform">
            <Form.Label>Game Platforms</Form.Label>
            <Form.Control type="text" name="platform" placeholder="Enter platforms (comma separated)" defaultValue={videoGame?.platform} />
        </Form.Group>
        <Button type="button" className="btn btn-secondary ms-2" onClick={closeForm}>Cancel</Button> 
        <Button type="submit" className="btn btn-primary" 
            disabled={updateVideoGame.isPending || createVideoGame.isPending}
        >Submit</Button>  
        </Form>
        
    </>
    
  )
}