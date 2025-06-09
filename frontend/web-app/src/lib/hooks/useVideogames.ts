import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";

export const useVideogames = () => {
    const queryClient = useQueryClient();
    const {data: videoGames, isPending} = useQuery({
        queryKey: ['videoGames'],
        queryFn: async () => {
            const response = await agent.get<VideoGame[]>('/videogames');
            return response.data;
        }
    });

    const updateVideoGame = useMutation({  
        mutationFn: async (game: VideoGame) => {
            await agent.put<VideoGame[]>('/videogames/' + game.id, game);
        },
        onSuccess: async () => {
            // Invalidate the query to refetch the data
            await queryClient.invalidateQueries({ queryKey: ['videoGames'] });
        }   
    });

    const createVideoGame = useMutation({  
        mutationFn: async (game: VideoGame) => {
            await agent.post<VideoGame[]>('/videogames', game);
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ['videoGames'] });
        }   
    })
    return {videoGames, isPending, updateVideoGame, createVideoGame};
}