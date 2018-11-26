%when installing cvx for the first time in a new environment run shared/cvx/cvx_setup.m
%cvx_setup.m will do three things:
%   1) add all required search paths for cvx functions (if on MAC or PC these are added permanently)
%   2) get the paths to cvx solvers and save the paths to `prefdir(1)\..\cvx_prefs.mat`
%   3) run a small optimization problem to make sure the first two steps completed successfully

%because I'm OCD I don't like it that cvx permanently adds paths to my environment. Therefore, I added
%the below cvx paths as part of this project. If these are already in the search path these includes
%won't do anything. If they aren't, then this will add them for the duration of the current session.
%Therefore, if you'd like, you can remove the cvx paths from your search path and use these instead.
%It should be noted, this doesn't get one around running cvx_setup since it also saves to the prefdir.

%In other news, because cvx doesn't store any state within its project folder, it seems safe to include
%the code repository within the repo without worrying about leaking information on any individual user

cvx_path = ['shared' filesep 'cvx'];

addpath(fullfile(fileparts(which(mfilename)), cvx_path));
addpath(fullfile(fileparts(which(mfilename)), cvx_path, 'functions', 'square_'));
addpath(fullfile(fileparts(which(mfilename)), cvx_path, 'functions', 'vec_'));
addpath(fullfile(fileparts(which(mfilename)), cvx_path, 'structures'));
addpath(fullfile(fileparts(which(mfilename)), cvx_path, 'lib'));
addpath(fullfile(fileparts(which(mfilename)), cvx_path, 'functions'));
addpath(fullfile(fileparts(which(mfilename)), cvx_path, 'commands'));
addpath(fullfile(fileparts(which(mfilename)), cvx_path, 'builtins'));

addpath(fullfile(fileparts(which(mfilename)),'shared', 'kernels'));

%use genpath to include all folders in a directory
%addpath(genpath(fullfile(fileparts(which(mfilename)),'shared')));
