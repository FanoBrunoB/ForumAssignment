

using CLI.UI;
using RepositoryContracts;
using FileRepositories;

Console.WriteLine("Starting CLI application...");
IUserRepository userRepository = new UserFileRepository();
ICommentRepository commentRepository = new CommentFileRepository();
IPostRepository postRepository = new PostFileRepository();
IForumRepository forumRepository = new ForumFileRepository();
IReactionRepository interactionRepository = new ReactionInMemoryRepository();

CliApp app = new CliApp(userRepository, commentRepository, postRepository, forumRepository, interactionRepository);
await app.StartAsync();
